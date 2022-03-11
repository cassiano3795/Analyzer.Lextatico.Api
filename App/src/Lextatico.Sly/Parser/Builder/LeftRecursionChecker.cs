using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Parser.Syntax.Grammar;

namespace Lextatico.Sly.Parser.Builder
{
    public class LeftRecursionChecker<T> where T : Token
    {
        public LeftRecursionChecker()
        {

        }

        private static List<string> BuildPath(List<string> current, string step)
        {
            var newPath = new List<string>();
            newPath.AddRange(current);
            newPath.Add(step);
            return newPath;
        }

        public static List<string> GetLeftClausesName(IClause<T> clause)
        {
            if (clause is NonTerminalClause<T> nonTerminal)
            {
                return new List<string> { nonTerminal.NonTerminalName };
            }

            return new List<string>();
        }

        public static List<string> GetLeftClausesName(Rule<T> rule, ParserConfiguration<T> configuration)
        {
            List<string> lefts = new List<string>();

            int i = 0;
            IClause<T> current = rule.Clauses[0] as IClause<T>;
            var currentLefts = GetLeftClausesName(current);
            bool stopped = false;
            while (i < rule.Clauses.Count && !stopped && currentLefts != null && currentLefts.Any())
            {
                stopped = !current.MayBeEmpty();
                lefts.AddRange(currentLefts);
                stopped = !current.MayBeEmpty();
                i++;
                if (i < rule.Clauses.Count())
                {
                    current = rule.Clauses[i];
                    currentLefts = GetLeftClausesName(current);
                }
                else
                {
                    current = null;
                    currentLefts = null;
                }
            }

            return lefts;

        }


        public static (bool foundRecursion, List<List<string>> recursions) CheckLeftRecursion(ParserConfiguration<T> configuration)
        {
            List<List<string>> recursions = new List<List<string>>();
            bool foundRecursion = false;
            foreach (var nonTerminal in configuration.NonTerminals)
            {
                var (found, recursion) = CheckLeftRecursion(configuration, nonTerminal.Value, new List<string>() { nonTerminal.Key });
                if (found)
                {
                    foundRecursion = true;
                    recursions.AddRange(recursion);
                }
            }



            return (foundRecursion, recursions);
        }

        public static (bool recursionFound, List<List<string>> recursion) CheckLeftRecursion(ParserConfiguration<T> configuration,
            NonTerminal<T> nonTerminal, List<string> currentPath)
        {
            var foundRecursion = false;
            List<List<string>> recursions = new List<List<string>>();

            var (found, path) = FindRecursion(currentPath);
            if (found)
            {
                return (true, new List<List<string>>() { currentPath });
            }

            var leftClauses = nonTerminal.Rules.SelectMany(x => GetLeftClausesName(x, configuration)).ToList();

            foreach (var leftClause in leftClauses)
            {
                if (configuration.NonTerminals.ContainsKey(leftClause))
                {
                    var newNonTerminal = configuration.NonTerminals[leftClause];
                    if (newNonTerminal != null)
                    {
                        var nPath = BuildPath(currentPath, leftClause);
                        var (foundRRuleRecursion, recursion) = CheckLeftRecursion(configuration, newNonTerminal, nPath);
                        if (foundRRuleRecursion)
                        {
                            foundRecursion = true;
                            recursions.AddRange(recursion);
                        }

                    }
                }
                else
                {
                    ;
                }
            }

            return (foundRecursion, recursions);


        }

        private static (bool, string) FindRecursion(List<string> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                string step = path[i];
                int next = path.LastIndexOf(step);
                if (next > i)
                {
                    string failure = string.Join(" > ", path.GetRange(i, next - i + 1));
                    return (true, failure);
                }
            }

            return (false, null);
        }
    }
}