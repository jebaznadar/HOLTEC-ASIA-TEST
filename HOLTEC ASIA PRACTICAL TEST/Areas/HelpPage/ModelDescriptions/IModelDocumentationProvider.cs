using System;
using System.Reflection;

namespace HOLTEC_ASIA_PRACTICAL_TEST.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}