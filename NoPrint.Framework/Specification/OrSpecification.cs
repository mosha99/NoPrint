namespace NoPrint.Framework.Specification;

public class OrSpecification : ISpecification
{
    private readonly ISpecification _leftSpecification;
    private readonly ISpecification _rightSpecification;

    public OrSpecification(ISpecification leftSpecification,ISpecification rightSpecification)
    {
        _leftSpecification = leftSpecification;
        _rightSpecification = rightSpecification;
    }

    public bool Satisfied()
    {
        return _leftSpecification.Satisfied() || _rightSpecification.Satisfied();
    }
}