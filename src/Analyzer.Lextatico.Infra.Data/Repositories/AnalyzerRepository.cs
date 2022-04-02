using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analyzer.Lextatico.Domain.Interfaces.Repositories;
using Analyzer.Lextatico.Domain.Models;
using Analyzer.Lextatico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using AnalyzerModel = Analyzer.Lextatico.Domain.Models.Analyzer;

namespace Analyzer.Lextatico.Infra.Data.Repositories
{
    public class AnalyzerRepository : BaseRepository<AnalyzerModel>, IAnalyzerRepository
    {
        public AnalyzerRepository(LextaticoContext lextaticoContext)
            : base(lextaticoContext)
        {
        }

        public async Task<AnalyzerModel> SelectAnalyzerByIdAndUserIdAsync(Guid id, Guid userId)
        {
            var analyzer = await _dataSet.FirstOrDefaultAsync(analyzer => analyzer.Id == id && analyzer.ApplicationUserId == userId);

            return analyzer;
        }

        public async Task<IEnumerable<AnalyzerModel>> SelectAnalyzersByIdsByUserIdAsync(IEnumerable<Guid> analyzersIds, Guid userId)
        {
            var analyzers = await _dataSet.Where(x => analyzersIds.Contains(x.Id) && x.ApplicationUserId == userId).ToListAsync();

            return analyzers;
        }

        public async Task<IEnumerable<AnalyzerModel>> SelectAnalyzersByUserIdAsync(Guid userId) =>
            await _dataSet.Where(f => f.ApplicationUserId == userId).ToListAsync();

        public async Task<(IEnumerable<AnalyzerModel>, int)> SelectAnalyzersPaggedByUserIdAsync(Guid userId, int page, int size)
        {
            var total = await _dataSet.CountAsync(c => c.ApplicationUserId == userId);

            var analyzers = await _dataSet.Where(f => f.ApplicationUserId == userId)
                .Skip((page - 1) * size).Take(size).ToListAsync();

            return (analyzers, total);
        }

        public override async Task<bool> UpdateAsync(AnalyzerModel analyzerFromDto)
        {
            var analyzerFromDb = await SelectByIdAsync(analyzerFromDto.Id);

            if (analyzerFromDb == null)
                return false;

            analyzerFromDto.SetCreatedAt(analyzerFromDb.CreatedAt);

            _lextaticoContext.Entry(analyzerFromDb).CurrentValues.SetValues(analyzerFromDto);

            UpdateAnalyzerTerminalTokens(analyzerFromDto, analyzerFromDb);

            await _lextaticoContext.SaveChangesAsync();

            UpdateNonTerminalTokens(analyzerFromDto, analyzerFromDb);

            await _lextaticoContext.SaveChangesAsync();

            UpdateNonTerminalTokensRules(analyzerFromDto, analyzerFromDb);

            await _lextaticoContext.SaveChangesAsync();

            UpdateNonTerminalTokensRuleClauses(analyzerFromDto, analyzerFromDb);

            var result = await _lextaticoContext.SaveChangesAsync();

            return result > 0;
        }

        private static void UpdateAnalyzerTerminalTokens(AnalyzerModel analyzerFromDto, AnalyzerModel analyzerFromDb)
        {
            var analyzerTerminalTokensNotInDb = analyzerFromDto.AnalyzerTerminalTokens
                            .Where(a => !analyzerFromDb.AnalyzerTerminalTokens.Select(s => s.TerminalTokenId).Contains(a.TerminalTokenId))
                            .ToList();

            var analyzerTerminalTokensNotInDto = analyzerFromDb.AnalyzerTerminalTokens
                .Where(a => !analyzerFromDto.AnalyzerTerminalTokens.Select(s => s.TerminalTokenId).Contains(a.TerminalTokenId))
                .ToList();

            analyzerTerminalTokensNotInDb.ForEach(a => analyzerFromDb.AnalyzerTerminalTokens.Add(a));

            analyzerTerminalTokensNotInDto.ForEach(a => analyzerFromDb.AnalyzerTerminalTokens.Remove(a));
        }

        private void UpdateNonTerminalTokens(AnalyzerModel analyzerFromDto, AnalyzerModel analyzerFromDb)
        {
            foreach (var nonTerminalTokenDto in analyzerFromDto.NonTerminalTokens)
            {
                nonTerminalTokenDto.SetAnalyzerId(analyzerFromDb.Id);

                var nonTerminalTokenDb = analyzerFromDb.NonTerminalTokens.FirstOrDefault(f => f.Id == nonTerminalTokenDto.Id);

                if (nonTerminalTokenDb != null)
                {
                    nonTerminalTokenDto.SetCreatedAt(nonTerminalTokenDb.CreatedAt);

                    _lextaticoContext.Entry(nonTerminalTokenDb).CurrentValues.SetValues(nonTerminalTokenDto);
                }
            }

            var nonTerminalTokensNotInDb = analyzerFromDto.NonTerminalTokens
                .Where(a => !analyzerFromDb.NonTerminalTokens.Select(s => s.Id).Contains(a.Id))
                .ToList();

            var nonTerminalTokensNotInDto = analyzerFromDb.NonTerminalTokens
                .Where(a => !analyzerFromDto.NonTerminalTokens.Select(s => s.Id).Contains(a.Id))
                .ToList();

            _lextaticoContext.AddRange(nonTerminalTokensNotInDb);

            _lextaticoContext.RemoveRange(nonTerminalTokensNotInDto.SelectMany(s => s.NonTerminalTokenRuleClauses));

            _lextaticoContext.RemoveRange(nonTerminalTokensNotInDto);
        }

        private void UpdateNonTerminalTokensRules(AnalyzerModel analyzerFromDto, AnalyzerModel analyzerFromDb)
        {
            foreach (var nonTerminalTokenDto in analyzerFromDto.NonTerminalTokens)
            {
                foreach (var nonTerminalTokenRuleDto in nonTerminalTokenDto.NonTerminalTokenRules)
                {
                    nonTerminalTokenRuleDto.SetNonTerminalTokenId(nonTerminalTokenDto.Id);

                    var nonTerminalTokenRuleDb = analyzerFromDb.NonTerminalTokens.SelectMany(s => s.NonTerminalTokenRules)
                    .FirstOrDefault(f => f.Id == nonTerminalTokenRuleDto.Id);

                    if (nonTerminalTokenRuleDb != null)
                    {
                        nonTerminalTokenRuleDto.SetCreatedAt(nonTerminalTokenRuleDb.CreatedAt);

                        _lextaticoContext.Entry(nonTerminalTokenRuleDb).CurrentValues.SetValues(nonTerminalTokenRuleDto);
                    }
                }
            }

            var nonTerminalTokensNotInDb = analyzerFromDto.NonTerminalTokens
                .SelectMany(s => s.NonTerminalTokenRules)
                .Where(a => !analyzerFromDb.NonTerminalTokens
                    .SelectMany(s => s.NonTerminalTokenRules)
                    .Select(s => s.Id)
                    .Contains(a.Id))
                .ToList();

            var nonTerminalTokensNotInDto = analyzerFromDb.NonTerminalTokens
                .SelectMany(s => s.NonTerminalTokenRules)
                .Where(a => !analyzerFromDto.NonTerminalTokens
                    .SelectMany(s => s.NonTerminalTokenRules)
                    .Select(s => s.Id)
                    .Contains(a.Id))
                .ToList();

            _lextaticoContext.AddRange(nonTerminalTokensNotInDb);

            _lextaticoContext.RemoveRange(nonTerminalTokensNotInDto);
        }

        private void UpdateNonTerminalTokensRuleClauses(AnalyzerModel analyzerFromDto, AnalyzerModel analyzerFromDb)
        {
            foreach (var nonTerminalTokenDto in analyzerFromDto.NonTerminalTokens)
            {
                foreach (var nonTerminalTokenRuleDto in nonTerminalTokenDto.NonTerminalTokenRules)
                {
                    foreach (var nonTerminalTokenRuleClauseDto in nonTerminalTokenRuleDto.NonTerminalTokenRuleClauses)
                    {
                        nonTerminalTokenRuleClauseDto.SetNonTerminalTokenRuleId(nonTerminalTokenRuleDto.Id);

                        var nonTerminalTokenRuleClauseDb = analyzerFromDb.NonTerminalTokens
                                                                .SelectMany(s => s.NonTerminalTokenRules).SelectMany(s => s.NonTerminalTokenRuleClauses)
                                                                .FirstOrDefault(f => f.Id == nonTerminalTokenRuleClauseDto.Id);

                        if (nonTerminalTokenRuleClauseDb != null)
                        {
                            nonTerminalTokenRuleClauseDto.SetCreatedAt(nonTerminalTokenRuleClauseDb.CreatedAt);

                            _lextaticoContext.Entry(nonTerminalTokenRuleClauseDb).CurrentValues.SetValues(nonTerminalTokenRuleClauseDto);
                        }
                    }
                }
            }

            var nonTerminalTokensNotInDb = analyzerFromDto.NonTerminalTokens
                .SelectMany(s => s.NonTerminalTokenRules)
                .SelectMany(s => s.NonTerminalTokenRuleClauses)
                .Where(a => !analyzerFromDb.NonTerminalTokens
                    .SelectMany(s => s.NonTerminalTokenRules)
                    .SelectMany(s => s.NonTerminalTokenRuleClauses)
                    .Select(s => s.Id)
                    .Contains(a.Id))
                .ToList();

            var nonTerminalTokensNotInDto = analyzerFromDb.NonTerminalTokens
                .SelectMany(s => s.NonTerminalTokenRules)
                .SelectMany(s => s.NonTerminalTokenRuleClauses)
                .Where(a => !analyzerFromDto.NonTerminalTokens
                    .SelectMany(s => s.NonTerminalTokenRules)
                    .SelectMany(s => s.NonTerminalTokenRuleClauses)
                    .Select(s => s.Id)
                    .Contains(a.Id))
                .ToList();

            _lextaticoContext.AddRange(nonTerminalTokensNotInDb);

            _lextaticoContext.RemoveRange(nonTerminalTokensNotInDto);
        }
    }
}
