﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using DSSAlternative.AppComponents;

namespace DSSAlternative.AHP
{
    public interface ITemplate : ICloneable
    {
        string Name { get; }
        string Description { get; }
        string Img { get; }

        List<Node> Nodes { get; }
        IEnumerable<INodeGroup> Groups { get; }
        TemplateRelation[] Relations { get; }

        ITemplate CloneThis();
    }

    public class Template : ITemplate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }

        public DateTime Creation { get; set; }
        public List<Node> Nodes { get; set; }
        public TemplateRelation[] Relations { get; set; } = Array.Empty<TemplateRelation>();


        [JsonIgnore]
        public IEnumerable<INodeGroup> Groups
        {
            get
            {
                var groupedNodes = Nodes.GroupBy(n => n.Group).OrderBy(g => g.Key);
                var groupIndexes = groupedNodes.Select(g => g.Key);

                List<NodeGroup> groups = new List<NodeGroup>();
                foreach (var index in groupIndexes)
                {
                    groups.Add(new NodeGroup(index, Nodes.Where(n => n.Group == index).ToArray()));
                }
                return groups;
            }
        }


        public Template()
        {

        }
        public Template(Node[] nodes)
        {
            Nodes = new List<Node>(nodes);
        }
        public Template(IEnumerable<Node> nodes, IEnumerable<INodeRelation> relations)
        {
            var goal = nodes.First(n => n.Level == 0);

            Name = $"Задача '{goal.Name}'";
            Description = $"Созданный шаблон на основе открытой задачи c {nodes.Count()} узлами";
            Img = "Images/settings.svg";
   
            Nodes = new List<Node>(nodes.Select(n => n.CloneThis()));
            Relations = relations.Select(r => new TemplateRelation() {
                Main = r.Main.Name,
                From = r.From.Name,
                To = r.To.Name,
                Value = r.Value
            }).ToArray();
            Creation = DateTime.Now;
        }
        public Template(IProject project)
            : this(project.ProblemActive.Hierarchy.OfType<Node>(), project.ProblemActive.RelationsRequired) { }

        public object Clone()
        {
            return CloneThis();
        }
        public ITemplate CloneThis()
        {
            IEnumerable<Node> copiedNodes = Nodes.Select(n => new Node(n.Level, n.Name, n.Group, n.GroupIndex));
            return new Template(copiedNodes.ToArray());
        }
    }

    public class TemplateRelation
    {
        public string Main { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Value { get; set; }
    }
}
