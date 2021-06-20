using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System.Collections.Generic;
using System.Diagnostics;
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



        public void CreateGraph(Automata<string> automata, string name)
        {
            var graph = new DotGraph(name, true);
            List<DotNode> dotNodes = new List<DotNode>();

         
            foreach (State node in automata.States)
            {
                DotNode graphnode;
                graphnode = new DotNode(node.ToString())
                {
                    Shape = DotNodeShape.Circle,
                    Label = node.ToString(),
                    FillColor = Color.Coral,
                    FontColor = Color.Black,
                    Style = DotNodeStyle.Solid,
                    Width = 0.5f,
                    Height = 0.5f
                };

                dotNodes.Add(graphnode);

            }



            foreach (DotNode node in dotNodes)
            {
                if (automata.StartStates.Contains(new State(node.Identifier)))
                {
                    node.FillColor = Color.LawnGreen;
                    node.Style = DotNodeStyle.Filled;
                }
                if (node.Identifier == "{}")
                {
                    node.FillColor = Color.Red;
                    node.Style = DotNodeStyle.Filled;
                }



                if (automata.FinalStates.Contains(new State(node.Identifier)))
                {
                    node.Shape = DotNodeShape.DoubleCircle;
                    node.FillColor = Color.White;
                    node.Style = DotNodeStyle.Filled;
                }
                graph.Elements.Add(node);
            }




            foreach (Transition<string> trans in automata.Transitions)
            {




                var myEdge = new DotEdge(trans.from.ToString(), trans.to.ToString())
                {
                    ArrowHead = DotEdgeArrowType.Vee,
                    ArrowTail = DotEdgeArrowType.Diamond,
                    Color = Color.Black,
                    FontColor = Color.Black,
                    Label = trans.symbol.ToString()
                };
                graph.Elements.Add(myEdge);
            }

     





            var dot = graph.Compile();
            dot = dot.Insert(12 + name.Length, "rankdir=LR;");
            File.WriteAllText(name + ".dot", dot);
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = ("dot");
                process.StartInfo.Arguments = $"-T svg {name}.dot -O {name}";
                process.Start();
            }
            //    batfile.AppendLine($"dot -T svg {name}.dot -O {name}");
            //batfile.AppendLine($"start {name}.dot.svg");
        }
    }
}
