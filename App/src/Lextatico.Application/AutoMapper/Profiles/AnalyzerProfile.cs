using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lextatico.Application.Dtos.Analyzer;
using Lextatico.Domain.Models;

namespace Lextatico.Application.AutoMapper.Profiles
{
    public class AnalyzerProfile : Profile
    {
        public AnalyzerProfile()
        {
            CreateMap<Analyzer, AnalyzerDto>().ReverseMap();

            CreateMap<AnalyzerWithTerminalTokensAndNonTerminalTokens, Analyzer>()
                .ForMember(analyzer => analyzer.AnalyzerTerminalTokens,
                    options => options.MapFrom(analyzerWithTerminalTokensAndNonTerminalTokens => analyzerWithTerminalTokensAndNonTerminalTokens.TerminalTokens))
                .ForMember(analyzer => analyzer.NonTerminalTokens,
                    options => options.MapFrom(analyzerWithTerminalTokensAndNonTerminalTokens => analyzerWithTerminalTokensAndNonTerminalTokens.NonTerminalTokens))
                .ReverseMap();
        }
    }
}
