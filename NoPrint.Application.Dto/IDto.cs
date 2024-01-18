namespace NoPrint.Application.Dto;

/// <summary>
/// Marker
/// </summary>
public interface IDto
{
}

public interface IDto<TModel> : IDto
{
    TModel ToModel();
    void FillFromModel(TModel model);
}