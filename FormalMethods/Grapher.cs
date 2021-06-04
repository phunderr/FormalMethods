using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace FormalMethods
{
    class Grapher
    {
        StringBuilder batfile = new StringBuilder();

        public void SaveToPDF()
        {
            File.WriteAllText("pdf.bat", batfile.ToString());
            System.Diagnostics.Process.Start("pdf.bat");
            batfile.Clear();
        }

        public void CreateGraph(List<Node> nodes, string name)
        {
            var graph = new DotGraph(name, true);
            List<DotNode> dotNodes = new List<DotNode>();
            foreach (var node in nodes)
            {
                DotNode graphnode;
                if (node.nodeType == NodeType.EndNode)
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.DoubleCircle,
                        Label = node.name,
                        FillColor = Color.Green,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Filled,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }
                else if (node.nodeType == NodeType.StartNode)
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.Circle,
                        Label = node.name,
                        FillColor = Color.LightBlue,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Filled,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }
                else
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.Circle,
                        Label = node.name,
                        FillColor = Color.Coral,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Solid,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }

                dotNodes.Add(graphnode);
                graph.Elements.Add(graphnode);
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                foreach (var item in node.connections)
                {
                    string nodename = item.node.name;
                    int number = 0;
                    for (int i2 = 0; i2 < dotNodes.Count; i2++)
                    {
                        if (dotNodes[i2].Label.Text.Equals(nodename))
                        {
                            number = i2;
                        }
                    }

                    var myEdge = new DotEdge(dotNodes[i], dotNodes[number])
                    {
                        ArrowHead = DotEdgeArrowType.Vee,
                        ArrowTail = DotEdgeArrowType.Diamond,
                        Color = Color.Black,
                        FontColor = Color.Black,
                        Label = item.letter.ToString()
                    };



                    graph.Elements.Add(myEdge);
                }
            }

            var dot = graph.Compile();
            dot = dot.Insert(10 + name.Length, "rankdir=\"LR\";");
            File.WriteAllText(name + ".dot", dot);
            batfile.AppendLine($"dot -T pdf {name}.dot -O");
            batfile.AppendLine($"start {name}.dot.pdf -O");
        }
    }
}

