using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Dtos;
using WebApi.Application.UseCases;

namespace WebApi.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpensesController(ExpenseService expenseService): ControllerBase
{
    // GET api/expenses/by-report/{reportId}?pageNumber=1
    [HttpGet("by-report/{reportId:guid}")]
    public async Task<ActionResult<PagedResultDto<ExpenseListItemDto>>> GetByReport(
        Guid reportId,
        [FromQuery] int pageNumber,
        CancellationToken ct)
    {
        var safePage = pageNumber<=0 ? 1: pageNumber;
        var result = await expenseService.ListByReportPagedAsync(reportId, safePage, pageSize:5, ct);

        return result == null ? NotFound() : Ok(result);
    }
    
    // POST api/expenses/by-report/{reportId}
    [HttpPost("by-report/{reportId:guid}")]
    public async Task<IActionResult> Create(
        Guid reportId,
        [FromBody] CreateExpenseDto dto,
        CancellationToken ct)
    {
        var id = await expenseService.CreateAsync(reportId, dto, ct);

        if (id == null)
        {
            return BadRequest();
        }
        
        return Created($"api/expenses/{id}", null);
    }
    
    // PUT api/expenses/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExpenseDto dto, CancellationToken ct)
    {
        var isUpdated = await expenseService.UpdateAsync(id, dto, ct);
        return isUpdated ? NoContent() : NotFound();
    }
    
    // DELETE api/expenses/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var isDeleted = await expenseService.DeleteAsync(id, ct);
        return isDeleted ? NoContent() : NotFound();
    }
}