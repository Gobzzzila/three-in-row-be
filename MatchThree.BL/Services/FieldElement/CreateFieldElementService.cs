using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Repository.MSSQL;

namespace MatchThree.BL.Services.FieldElement;

public class CreateFieldElementService(MatchThreeDbContext context) 
    : ICreateFieldElementService
{
    private readonly MatchThreeDbContext _context = context;
    
    
}