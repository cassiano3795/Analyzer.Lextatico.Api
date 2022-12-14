using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Analyzer.Lextatico.Application.Dtos.NonTerminalToken;
using Analyzer.Lextatico.Domain.Models;
using Microsoft.Extensions.Options;

namespace Analyzer.Lextatico.Application.AutoMapper.Profiles
{
    public class NonTerminalTokenProfile : Profile
    {
        public NonTerminalTokenProfile()
        {
            CreateMap<NonTerminalToken, NonTerminalTokenDto>().ReverseMap();

            CreateMap<NonTerminalToken, NonTerminalTokenWithRulesAndClausesDto>().ReverseMap();

            CreateMap<NonTerminalTokenWithRulesAndClausesDto, NonTerminalToken>().ReverseMap();

            CreateMap<NonTerminalTokenRuleWithClausesDto, NonTerminalTokenRule>().ReverseMap();

            CreateMap<NonTerminalTokenRuleClauseDto, NonTerminalTokenRuleClause>()
                .ForMember(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.TerminalTokenId,
                    options => options.AllowNull())
                .ForMember(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalTokenId,
                    options => options.AllowNull())
                .ReverseMap();
        }
    }
}
