using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lextatico.Application.Dtos.TerminalToken;
using Lextatico.Domain.Models;

namespace Lextatico.Application.AutoMapper.Profiles
{
    public class TerminalTokenProfile : Profile
    {
        public TerminalTokenProfile()
        {
            CreateMap<TerminalToken, TerminalTokenDto>().ReverseMap();

            CreateMap<TerminalTokenDto, AnalyzerTerminalToken>()
                .ForMember(analyzerTerminalToken => analyzerTerminalToken.Id,
                    options => options.Ignore())
                .ForMember(analyzerTerminalToken => analyzerTerminalToken.TerminalTokenId,
                    options => options.MapFrom(terminalTokenDto => terminalTokenDto.Id))
                .ReverseMap()
                .ForMember(terminalTokenDto => terminalTokenDto.Id,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.Id))
                .ForMember(terminalTokenDto => terminalTokenDto.Name,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.Name))
                .ForMember(terminalTokenDto => terminalTokenDto.ViewName,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.ViewName))
                .ForMember(terminalTokenDto => terminalTokenDto.Resume,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.Resume))
                .ForMember(terminalTokenDto => terminalTokenDto.TokenType,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.TokenType))
                .ForMember(terminalTokenDto => terminalTokenDto.IdentifierType,
                    options => options.MapFrom(analyzerTerminalToken => analyzerTerminalToken.TerminalToken.IdentifierType));
        }
    }
}
