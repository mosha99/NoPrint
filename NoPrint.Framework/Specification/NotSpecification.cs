namespace NoPrint.Framework.Specification;

public class NotSpecification : ISpecification
{
    private readonly ISpecification _leftSpecification;

    public NotSpecification(ISpecification leftSpecification)
    {
        _leftSpecification = leftSpecification;
    }

    public bool Satisfied()
    {
        return !_leftSpecification.Satisfied();
    }
}