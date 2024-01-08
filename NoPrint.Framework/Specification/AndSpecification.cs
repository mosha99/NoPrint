namespace NoPrint.Framework.Specification;

public class AndSpecification : ISpecification
{
    private readonly ISpecification _leftSpecification;
    private readonly ISpecification _rightSpecification;

    public AndSpecification(ISpecification leftSpecification,ISpecification rightSpecification)
    {
        _leftSpecification = leftSpecification;
        _rightSpecification = rightSpecification;
    }

    public bool Satisfied()
    {
        return _leftSpecification.Satisfied() && _rightSpecification.Satisfied();
    }
}