using Microsoft.EntityFrameworkCore;
using {{ ProjectName }}.Persistence.Context;
using {{ ProjectName }}.Persistence.Entities;

namespace {{ ProjectName }}.Persistence.Repositories;
public class {{ EntityName }}Repository(AppDbContext context) : BaseRepository<{{ EntityName }}Entity, Guid>(context);
