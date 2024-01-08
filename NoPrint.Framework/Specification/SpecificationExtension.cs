namespace NoPrint.Framework.Specification;

public static class SpecificationExtension
{
    public static ISpecification And(this ISpecification left , ISpecification right) => new AndSpecification(left , right);
    public static ISpecification Or(this ISpecification left , ISpecification right) => new OrSpecification(left , right);
    public static ISpecification Not(this ISpecification left ) => new NotSpecification(left);
}