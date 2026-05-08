namespace Application.Client;

public class MVCBrandsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<BrandUpsertDto> _validator;

    public MVCBrandsController(IUnitOfWork unitOfWork, IValidator<BrandUpsertDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.Repository<ProductBrand>().GetAllAsync(cancellationToken);
        return View(list.OrderBy(b => b.Name).ToList());
    }

    public IActionResult Create()
    {
        return View(new BrandUpsertDto(string.Empty, string.Empty));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            return View(dto);
        }

        var entity = dto.Adapt<ProductBrand>();
        await _unitOfWork.Repository<ProductBrand>().AddAsync(entity, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        ViewBag.BrandId = entity.Id;
        return View(entity.Adapt<BrandUpsertDto>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, BrandUpsertDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            ViewBag.BrandId = id;
            return View(dto);
        }

        var entity = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        dto.Adapt(entity);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();

        var inUse = await _unitOfWork.Repository<Product>().GetCountAsync(p => p.BrandId == id, cancellationToken);
        if (inUse > 0)
        {
            TempData["ErrorMessage"] = Resource.CannotDeleteBrandInUse;
            return RedirectToAction(nameof(Index));
        }

        await _unitOfWork.Repository<ProductBrand>().DeleteAsync(entity);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
