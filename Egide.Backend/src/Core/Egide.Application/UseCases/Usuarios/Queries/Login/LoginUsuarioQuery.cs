using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.Login;
public record LoginUsuarioQuery(string Email, string Senha) : IRequest<LoginResponse>;
