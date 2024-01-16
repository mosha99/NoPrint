using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Tools;
using NoPrint.Users.Share;

namespace NoPrint.Users.Domain.ValueObjects;

public class Visitor
{
    private const int CodeExpireMin = 3;
    private const int TryTime = 1;
    public Visitor() { }

    public int? Code { get; private set; }
    public DateTime? CodeGenerateTime { get; private set; }

    public void Clear()
    {
        CodeGenerateTime = null;
        Code = null;
    }

    public string GenerateCode(ILoginAbleByPhone loginAbleByPhone)
    {
        loginAbleByPhone.ValidationCheck(x => x is not null, "Error_CantFindVisitor");

        CodeGenerateTime.ValidationCheck(x => x is null || x.Value.AddMinutes(TryTime) < DateTime.Now, "Error_NotValid");

        var ensureCode = new TimeScopeCodeGenerator(CodeExpireMin).GetCode();

        return GenerateCode(ensureCode,loginAbleByPhone, false);
    }
    public void Validate(string code , ILoginAbleByPhone loginAbleByPhone)
    {
        CodeGenerateTime.ValidationCheck(x => x is not null, "Error_NotValid");
        loginAbleByPhone.ValidationCheck(x => x is not null, "Error_NotValid");
        CodeGenerateTime.Value.AddMinutes(CodeExpireMin).ValidationCheck(x => x > DateTime.Now, "Error_NotValid");
        bool isValid = new TimeScopeCodeGenerator(CodeExpireMin).CodeIsValid(code, x=> GenerateCode(x,loginAbleByPhone));
        isValid.ValidationCheck(x => x, "Error_NotValid");
    }
    private string GenerateCode(int ensureCode, ILoginAbleByPhone loginAbleByPhone) => GenerateCode(ensureCode,loginAbleByPhone, true);
    private string GenerateCode(int ensureCode, ILoginAbleByPhone loginAbleByPhone, bool forValidate)
    {
        if (!forValidate)
        {
            Code = new Random().Next(1000, 9999);
            CodeGenerateTime = DateTime.Now;
        }

        var numberCode = 1;

        loginAbleByPhone.PhoneNumber.Where(x => x != '0').ToList().ForEach(x => numberCode *= x);

        string result = (Code * numberCode * ensureCode).ToString();

        var rl = result.Take(4).ToList();

        rl.Add(result[^1]);
        rl.Add(result[^3]);

        if (rl[3] == '0') rl[3] = rl[2];
        if (rl[3] == '0') rl[3] = '6';

        return $"{rl[3]}{rl[5]}{rl[1]}{rl[4]}";
    }
}