using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Dtos;
using WebApi.Application.UseCases;

namespace WebApi.Controllers;

[ApiController]
[Route("api/expense-reports")]
public sealed class ExpenseReportsController(ExpenseReportService expenseReportService): ControllerBase
{
    // GET api/expense-reports
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExpenseReportListItemDto>>> GetAll(CancellationToken ct)
    {
        var reports = await expenseReportService.ListAsync(ct);
        return Ok(reports);
    }
    
    // GET api/expense-reports/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExpenseReportDetailsDto>> GetById(Guid id, CancellationToken ct)
    {
        var report = await expenseReportService.GetAsync(id, ct);
        return report == null ? NotFound() : Ok(report);
    }
    
    // POST api/expense-reports
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExpenseReportDto dto, CancellationToken ct)
    {
        var (id, error) = await expenseReportService.CreateAsync(dto, ct);
        if (error != null )
        {
            return BadRequest(error.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    // DELETE api/expense-reports/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var isDeleted = await expenseReportService.DeleteAsync(id, ct);
        return isDeleted? NoContent() : NotFound("Expense report specified not found");
    }
}