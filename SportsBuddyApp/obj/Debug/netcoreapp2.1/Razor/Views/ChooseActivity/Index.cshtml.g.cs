#pragma checksum "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cac6d1652fe9e40ec1945c0d21de1b2adaa76329"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ChooseActivity_Index), @"mvc.1.0.view", @"/Views/ChooseActivity/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ChooseActivity/Index.cshtml", typeof(AspNetCore.Views_ChooseActivity_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\_ViewImports.cshtml"
using SportsBuddy;

#line default
#line hidden
#line 3 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\_ViewImports.cshtml"
using SportsBuddy.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cac6d1652fe9e40ec1945c0d21de1b2adaa76329", @"/Views/ChooseActivity/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a356c096b5bd153f8f4f1f0dbbf55edef124482f", @"/Views/_ViewImports.cshtml")]
    public class Views_ChooseActivity_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RecretionalActivity>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Choose", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
    int count = 0;

#line default
#line hidden
            BeginContext(153, 53, true);
            WriteLiteral("\r\n\r\n<div class=\"col-sm-8\">\r\n    <h2>Activities</h2>\r\n");
            EndContext();
            BeginContext(276, 133, true);
            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>SL</th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(410, 48, false);
#line 21 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
               Write(Html.DisplayNameFor(model => model.ActivityName));

#line default
#line hidden
            EndContext();
            BeginContext(458, 112, true);
            WriteLiteral("\r\n                </th>\r\n                <th>Action</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 27 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
             foreach (var item in Model)
            {
                count++;

#line default
#line hidden
            BeginContext(653, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(700, 5, false);
#line 31 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
                   Write(count);

#line default
#line hidden
            EndContext();
            BeginContext(705, 57, true);
            WriteLiteral("</td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(763, 47, false);
#line 33 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.ActivityName));

#line default
#line hidden
            EndContext();
            BeginContext(810, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(889, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2ad28044d5f74fd89769eb86acf526fe", async() => {
                BeginContext(952, 6, true);
                WriteLiteral("Choose");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-activityId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 36 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"
                                                         WriteLiteral(item.ActivityId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["activityId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-activityId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["activityId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(962, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 39 "G:\Personal\Job_Tests\SportsBuddyApp\sportsbuddyapp\SportsBuddyApp\Views\ChooseActivity\Index.cshtml"

            }

#line default
#line hidden
            BeginContext(1031, 42, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RecretionalActivity>> Html { get; private set; }
    }
}
#pragma warning restore 1591