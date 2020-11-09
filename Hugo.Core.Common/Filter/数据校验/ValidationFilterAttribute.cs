using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 数据校验特性
    /// </summary>
    public class ValidationFilterAttribute : BaseActionFilterAsync//ActionFilterAttribute
    {
        /// <summary>
        /// 重写 - 在执行操作Action方法前执行调用 - 数据校验
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        /// <returns></returns>
        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrorList = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                context.Result = Error(string.Join("； ", validationErrorList), 0);
            }

            await Task.CompletedTask;
        }

    }

}

/*
属性参数特性[Attribute]说明：https://docs.microsoft.com/zh-cn/dotnet/api/system.componentmodel.dataannotations?redirectedfrom=MSDN&view=netcore-3.1

AssociatedMetadataTypeTypeDescriptionProvider	
通过添加在关联类中定义的特性和属性信息，从而扩展某个类的元数据信息。
AssociationAttribute	
指定实体成员表示数据关系（如外键关系）。
CompareAttribute	
提供用于比较两个属性的特性。
ConcurrencyCheckAttribute	
指定属性参与乐观并发检查。
CreditCardAttribute	
指定数据字段值是信用卡号。
CustomValidationAttribute	
指定用于验证属性或类实例的自定义验证方法。
DataTypeAttribute	
指定要与数据字段关联的其他类型的名称。
DisplayAttribute	
提供允许为实体分部类的类型和成员指定可本地化字符串的通用特性。
DisplayColumnAttribute	
指定作为外键列显示在被引用表中的列。
DisplayFormatAttribute	
指定 ASP.NET 动态数据如何显示数据字段以及如何设置数据字段的格式。
EditableAttribute	
指示数据字段是否可编辑。
EmailAddressAttribute	
验证电子邮件地址。
EnumDataTypeAttribute	
启用 .NET Framework 枚举，以映射到数据列。
FileExtensionsAttribute	
验证文件扩展名。
FilterUIHintAttribute	
表示用于指定列的筛选行为的特性。
KeyAttribute	
表示一个或多个用于唯一标识实体的属性。
MaxLengthAttribute	
指定属性中允许的数组或字符串数据的最大长度。
MetadataTypeAttribute	
指定要与数据模型类关联的元数据类。
MinLengthAttribute	
指定属性中允许的数组或字符串数据的最小长度。
PhoneAttribute	
指定数据字段值是格式标准的电话号码。
RangeAttribute	
为数据字段的值指定数值范围约束。
RegularExpressionAttribute	
指定 ASP.NET 动态数据中的数据字段值必须与指定的正则表达式匹配。
RequiredAttribute	
指定数据字段值是必需的。
ScaffoldColumnAttribute	
指定类或数据列是否使用基架。
StringLengthAttribute	
指定数据字段中允许的字符的最小长度和最大长度。
TimestampAttribute	
列的数据类型指定为行版本。
UIHintAttribute	
指定动态数据用来显示数据字段的模板或用户控件。
UrlAttribute	
提供 URL 验证。
ValidationAttribute	
充当所有验证特性的基类。
ValidationContext	
描述执行验证检查的上下文。
ValidationException	
表示在使用 ValidationAttribute 类的情况下验证数据字段时发生的异常。
ValidationResult	
表示验证请求结果的容器。
Validator	
定义一个帮助器类，在与对象、属性和方法关联的 ValidationAttribute 特性中包含此类时，可使用此类来验证这些项。


Required 必填 示例：[Required(ErrorMessage = "ID是必填项")] 需要注意除了string类型的其他的值类型由于会赋予默认值, 所以加这个属性的时候值类型字段需要设置为可为空 例如 int? Id {get;set;}
Range 范围校验 示例：[Range(1,99999999,ErrorMessage ="请输入正确的页码")]
Compare 比较 与指定的字段值进行比较 示例：[Compare("MyOtherProperty")]两个属性必须相同值，比如我们要求用户重复输入两次邮件地址时有用
CreditCard 信用卡号
EmailAddress 是否为邮件
EnumDataType 校验枚举类型 示例:  [EnumDataType(typeof(EnumModels.ResponseHttpCode),ErrorMessage = "未知的类型")]
MaxLength 最大长度, 示例: [MaxLength(50,ErrorMessage = "昵称不能超过50个字")]
MinLength 最小长度 , 示例: [MinLength(2,ErrorMessage = "昵称不能少于2个字")]
StringLength 字符串长度不能超过给定的最大长度，也可以指定最小长度. 示例: [StringLength(50, ErrorMessage = "昵称只能介于2-50个字", MinimumLength = 2)]
Url url格式, 示例: [Url(ErrorMessage = "链接格式错误")]
RegularExpression 正则表达式 示例: [RegularExpression(@"^[1]{1}[3,4,5,6,7,8,9]{1}\d{9}$", ErrorMessage = "手机号码格式错误")]
*/
