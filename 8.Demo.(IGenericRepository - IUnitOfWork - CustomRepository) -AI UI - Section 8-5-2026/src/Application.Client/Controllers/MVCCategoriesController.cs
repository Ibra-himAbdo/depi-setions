namespace Application.Client;

public class MVCCategoriesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoryUpsertDto> _validator;

    public MVCCategoriesController(IUnitOfWork unitOfWork, IValidator<CategoryUpsertDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.Repository<ProductCategory>().GetAllAsync(cancellationToken);
        return View(list.OrderBy(c => c.Name).ToList());
    }

    public IActionResult Create()
    {
        return View(new CategoryUpsertDto(string.Empty, string.Empty));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            return View(dto);
        }

        var entity = dto.Adapt<ProductCategory>();
        await _unitOfWork.Repository<ProductCategory>().AddAsync(entity, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        ViewBag.CategoryId = entity.Id;
        return View(entity.Adapt<CategoryUpsertDto>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, CategoryUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            ViewBag.CategoryId = id;
            return View(dto);
        }

        var entity = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        dto.Adapt(entity);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        var inUse = await _unitOfWork.Repository<Product>().GetCountAsync(p => p.CategoryId == id, cancellationToken);
        if (inUse > 0)
        {
            TempData["ErrorMessage"] = Resource.CannotDeleteCategoryInUse;
            return RedirectToAction(nameof(Index));
        }

        await _unitOfWork.Repository<ProductCategory>().DeleteAsync(entity);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
