﻿using ApiTemplate.Application.Common;
using ApiTemplate.Application.Requests.Common;
using ApiTemplate.Application.Responses.Common;
using ApiTemplate.Domain.Validation;
using OneOf;

namespace ApiTemplate.Application.Commands.Common;

public class DeleteHandler<TEntity> : IDeleteHandler<TEntity>
    where TEntity : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ValidationResult _validationResult;

    public DeleteHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validationResult = new ValidationResult();
    }

    public async Task<OneOf<DeleteResponse<GetByIdRequest>, ValidationResult>> Handle(GetByIdRequest request)
    {
        await _unitOfWork.BeginTransaction();
        var dbSet = _unitOfWork.DbSet<TEntity>();
        var entity = await dbSet.FindAsync(request.Id);

        if (entity == null)
            return _validationResult.Add("Not Found", ValidationResult.ValidationErrorCode.NotFound);

        dbSet.Remove(entity);
        if (_validationResult.IsValid) await _unitOfWork.Commit();

        return new DeleteResponse<GetByIdRequest> { Id = request.Id, Value = request };
    }
}
