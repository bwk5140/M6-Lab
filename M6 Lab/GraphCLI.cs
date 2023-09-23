using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace M6_Lab
{
    public class GraphCLI
    {
        private struct CLICommand
        {
            public Action<GraphCLI, int[]> Action { get; set; }
            public string Signature { get; set; }
            public string Desc { get; set; }
            public int ArgCount { get; set; }
        }

        private Graph_Manager manager;
        private IDictionary<string, CLICommand> commands;

        public void Help()
        {
            foreach (var entry in commands)
            {
                var sig = entry.Value.Signature;
                Console.WriteLine("{0}{1} :: {2}", entry.Key, 
                    string.IsNullOrWhiteSpace(sig) ? "" : (" " + sig),
                    entry.Value.Desc);
            }
        }

        public GraphCLI(Graph_Manager manager)
        {
            this.manager = manager;
            commands = new Dictionary<string, CLICommand>()
            {
                ["help"] = new CLICommand()
                { 
                    Action = (GraphCLI cli, int[] args) => cli.Help(),
                    Desc = "Outputs this command",
                    Signature = "",
                    ArgCount = 0,
                },
                ["list"] = new CLICommand()
                {
                    Action = (GraphCLI cli, int[] args) =>
                    {
                        Console.WriteLine(string.Join(", ", manager.GraphIds));
                    },
                    Signature = "",
                    Desc = "List all graph ids",
                    ArgCount = 0,
                },
                ["print"] = new CLICommand()
                { 
                    Action = (GraphCLI cli, int[] args) => {
                        var graph = manager.GetGraph(args[0]);
                        if (graph != null)
                        {
                            graph.Print();
                        }
                        else
                        {
                            Console.WriteLine("No graph with ID {0}", args[0]);
                        }
                    },
                    Signature = "id",
                    Desc = "Print out graph with given id",
                    ArgCount = 1,
                },
                ["revise"] = new CLICommand()
                { 
                    Action = (GraphCLI cli, int[] args) => {
                        var graph = manager.GetGraph(args[0]);
                        if (graph != null)
                        {
                            graph.Revise(args[1]);
                        }
                        else
                        {
                            Console.WriteLine("No graph with ID {0}", args[0]);
                        }
                    },
                    Signature = "graph_id",
                    Desc = "Edits vertex or edge in graph with graph_id",
                    ArgCount = 1,
                },
                ["copy"] = new CLICommand()
                {
                    Action = (GraphCLI cli, int[] args) =>
                    {
                        manager.Clone(args[0]);
                    },
                    Signature = "id",
                    Desc = "Copies graph with id",
                    ArgCount = 1,
                },
                ["vertex"] = new CLICommand()
                {
                    Action = (GraphCLI cli, int[] args) =>
                    {
                        var graph = manager.GetGraph(args[0]);
                        if (graph != null)
                        {
                            graph.AddVertex(args[1], args[2]);
                        }
                        else
                        {
                            Console.WriteLine("No graph with ID {0}", args[0]);
                        }
                    },
                    Signature = "graph_id x y",
                    Desc = "Add new vertex to graph with given coordinates",
                    ArgCount = 3,
                },
                ["edge"] = new CLICommand()
                {
                    Action = (GraphCLI cli, int[] args) =>
                    {
                        var graph = manager.GetGraph(args[0]);
                        if (graph != null && graph.FindVertex(args[1]) != null && graph.FindVertex(args[2]) != null)
                        {
                            graph.AddEdge(graph.FindVertex(args[1]), graph.FindVertex(args[2]));
                        }
                        else
                        {
                            Console.WriteLine("Invalid graph or vertex ID", args[0]);
                        }
                    },
                    Signature = "graph_id x y",
                    Desc = "Add new vertex to graph with given coordinates",
                    ArgCount = 3,
                },
                ["exit"] = new CLICommand()
                {
                    Action = (GraphCLI cli, int[] args) =>
                    {
                        Environment.Exit(0);
                    },
                    Signature = "",
                    Desc = "Exit application",
                    ArgCount = 0,
                },
            };
        }

        public void RunLoop()
        {
            Help();
            while (true)
            {
                Console.Write("> ");
                string rawInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(rawInput))
                {
                    continue;
                }

                string[] input = rawInput.Split(' ');
                string command = input[0];
                if (commands.ContainsKey(command))
                {
                    int[] args = { };
                    try
                    {
                        args = input.Skip(1).Select(int.Parse).ToArray();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Could not parse arguments as integers");
                        continue;
                    }
                    if (args.Length != commands[command].ArgCount) 
                    {
                        Console.WriteLine("Incorrect number of arguments.  Expected {0} instead of {1}",
                            commands[command].ArgCount, args.Length);
                        continue;
                    }
                    commands[command].Action(this, args);
                }
                else
                {
                    Console.WriteLine("Command '{0}' not found", command);
                }
            }
        }
    }
}
