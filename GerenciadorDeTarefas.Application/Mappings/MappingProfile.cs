using AutoMapper;
using GerenciadorDeTarefas.Domain.Entities;
using GerenciadorDeTarefas.Application.DTOs;

namespace GerenciadorDeTarefas.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tarefa, TarefaDTO>().ReverseMap();
    }
}