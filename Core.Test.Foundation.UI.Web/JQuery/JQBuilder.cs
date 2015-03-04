//----------------------------------------------------------------------- // 

//<copyright file="JQBuilder.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQBuilder
    {
        public static string GetIncreaseValueScript(string path, double value)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var control =jQuery(\"{0}\"); var val=control.val()+ {1}; control.val(val) ", path, value);
            }
            else
            {
                return string.Format("var control ={0}; var val=control.val()+ {1}; control.val(val) ", path, value);
            }
        }

        public static string GetEventClickScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("jQuery(\"{0}\")[0].click()", path);
            }
            else
            {
                return string.Format("return {0}[0].click()", path);
            }
        }

        public static string GetDecreaseValueScript(string path, double value)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var control =jQuery(\"{0}\"); var val=control.val()- {1}; control.val(val) ", path, value);
            }
            else
            {
                return string.Format("var control ={0}; var val=control.val()- {1}; control.val(val)", path, value);
            }
        }

        public static string GetCurrentValueScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").val()", path);
            }
            else
            {
                return string.Format("return {0}.val()", path);
            }
        }

        public static string GetClickScript(string path)
        {          
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("jQuery(\"{0}\").click()", path);
            }
            else
            {
                return string.Format("return {0}.click()", path);
            }
        }

        public static string GetGetRowDataScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var text=''; jQuery(\"{0}\").each(function(){text+=\",\"+$(this).text();});return text.substring(1, text.length);", path);
            }
            else
            {
                return string.Format("var text='';{0}.each(function(){text+=\",\"+$(this).text();});return text.substring(1, text.length);", path);
            }
        }

        public static string GetSetTextAreaTextScript(string path, string text)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele = jQuery(\"{0}\");ele.attr('value', '{1}');ele.change()", path, text);
            }
            else
            {
                return string.Format("var ele = {0};ele.attr('value', '{1}');ele.change()", path, text);
            }
        }

        public static string GetGetTextAreaTextScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele = jQuery(\"{0}\"); return ele.attr('value');", path);
            }
            else
            {
                return string.Format("var ele = {0}; return ele.attr('value');", path);
            }
        }

        public static string GetSetTextScript(string path, string text)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele = jQuery(\"{0}\");ele.focus();ele.val('{1}');ele.trigger('change')", path, text);
            }
            else
            {
                return string.Format("var ele = {0};ele.focus();ele.val('{1}');ele.trigger('change')", path, text);
            }
        }

        public static string GetTextScript(string path)
        {         
            if (!path.StartsWith("jQuery"))
            {
                return string.Format(" return jQuery(\"{0}\").val()", path);
            }
            else
            {
                return string.Format(" return {0}.val()", path);
            }
        }

        public static string GetVisibleScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format(" return jQuery(\"{0}\").is(':visible')", path);
            }
            else
            {
                return string.Format(" return {0}.is(':visible')", path);
            }
        }

        public static string GetCSSVisibleScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format(" return jQuery(\"{0}\").css('visibility')", path);
            }
            else
            {
                return string.Format(" return {0}.css('visibility')", path);
            }
        }

        public static string GetDisabledScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").is(':disabled')", path);
            }
            else
            {
                return string.Format("return {0}.is(':disabled')", path);
            }
        }

        public static string GetCheckedScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").is(':checked')", path);
            }
            else
            {
                return string.Format("return jQuery(\"{0}\").is(':checked')", path);
            }
        }

        public static string GetFireEventScript(string path, string eventName)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("jQuery(\"{0}\").trigger('{1}')", path, eventName);
            }
            else
            {
                return string.Format("{0}.trigger('{1}')", path, eventName);
            }
        }

        public static string GetInnerHtmlScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").html()", path);
            }
            else
            {
                return string.Format("return {0}.html()", path);
            }
        }

        public static string GetGetAttributeScript(string path, string attrName)
        {            
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").attr('{1}')", path, attrName);
            }
            else
            {
                return string.Format("return {0}.attr('{1}')", path, attrName);
            }
        }

        public static string GetSetAttributeScript(string path, string attrName, string attrValue)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("jQuery(\"{0}\").attr('{1}', '{2}')", path, attrName, attrValue);
            }
            else
            {
                return string.Format("{0}.attr('{1}', '{2}')", path, attrName, attrValue);
            }
        }

        public static string GetPropertyScript(string path, string propertyName)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").prop('{1}')", path, propertyName);
            }
            else
            {
                return string.Format("return {0}.prop('{1}')", path, propertyName);
            }
        }

        public static string GetLocationScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele = jQuery(\"{0}\"); var pos = ele.offset(); return (pos.left)+\",\"+(pos.top)+\",\"+ele.width()+\",\"+ele.height() ", path);
            }
            else
            {
                return string.Format("var ele ={0}; var pos = ele.offset(); return (pos.left)+\",\"+(pos.top)+\",\"+ele.width()+\",\"+ele.height() ", path);
            }
        }

        public static string GetGetInnerTextScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").text()", path);
            }
            else
            {
                return string.Format("return {0}.text()", path);
            }
        }
        public static string GetGetInnerTextScriptIgnoreChild(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").clone().children().remove().end().text()", path);
            }
            else
            {
                return string.Format("return {0}.clone().children().remove().end().text()", path);
            }
        }

        public static string GetSelectedValueScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").val()", path);
            }
            else
            {
                return string.Format("return {0}.val()", path);
            }
        }

        public static string GetSelectedDDItemScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var val = jQuery(\"{0}\").val();var text = jQuery(\"{1} option[value='\"+val+\"']\").text().trim(); return val+\";\"+text", path, path);
            }
            else
            {
                return string.Format("var val = {0}.val();var text = jQuery(\"{1} option[value='\"+val+\"']\").text().trim(); return val+\";\"+text", path, path);
            }
        }

        public static string GetSelectValueScript(string path, string value)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele=jQuery(\"{0}\"); ele.val('{1}');ele.change()", path, value);
            }
            else
            {
                return string.Format("var ele={0}; ele.val('{1}');ele.change()", path, value);
            }
        }

        public static string GetSelectItemByTextScript(string path, string text)
        {
            string s = "";
            if (!path.StartsWith("jQuery"))
            {
                s = "var text = \"{0}\";\n" +
                    "var path = \"{1}\"\n" +
                    "var items = jQuery(path+\" option:contains('\"+text+\"')\");\n" +
                    "var val='';\n" +
                    "for(var i=0;i<items.length;i++){\n" +
                    "if(jQuery(items[i]).text().trim() == text){\n" +
                    "val = jQuery(items[i]).attr('value');\n" +
                    "break;}\n" +
                    "}\n" +
                    "if(val!=''){\n" +
                    "jQuery(path).val(val); jQuery(path).change()\n" +
                    "}";
            }
            else
            {
                s = "var text = \"{0}\";\n" +
                    "var items = {1}.find(\"option:contains('\"+text+\"')\");\n" +
                    "var val='';\n" +
                    "for(var i=0;i<items.length;i++){\n" +
                    "if(jQuery(items[i]).text().trim() == text){\n" +
                    "val = jQuery(items[i]).attr('value');\n" +
                    "break;}\n" +
                    "}\n" +
                    "if(val!=''){\n" +
                    "{1}.val(val); {1}.change()\n" +
                    "}";
            }

            return string.Format(s, text, path);
        }

        public static string GetDDItemTextScript(string path, string value)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0} option[value='{1}']\").text()", path, value);
            }
            else
            {
                return string.Format("return {0}.find(\"option[value='{1}']\").text()", path, value);
            }
        }

        public static string GetElementCountScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").length", path);
            }
            else
            {
                return string.Format("return {0}.length", path);
            }
        }

        public static string GetElementIndexScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").index()", path);
            }
            else
            {
                return string.Format("return {0}.index()", path);
            }
        }

        public static string GetFocusScript(string path)
        {          
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("jQuery(\"{0}\").focus()", path);
            }
            else
            {
                return string.Format("{0}.focus()", path);
            }
        }

        public static string GetAllDDValuesTextScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format(" return jQuery(\"{0} option\").map(function(i, n)", path) + " {  return n.text;}).Get().join(\", \")";
            }
            else
            {
                return string.Format(" return {0}.map(function(i, n)", path) + " {  return n.text;}).Get().join(\", \")";
            }
        }

        public static string GetAllDDValuesScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0} option\").map(function(i, n)", path) + " {  return n.value;}).get().join(\", \")";
            }
            else
            {
                return string.Format("return {0}.find(\"option\").map(function(i, n)", path) + " {  return n.value;}).get().join(\", \")";
            }
        }

        public static string GetDDItems(string path)
        {
            string query = "";

            if (!path.StartsWith("jQuery"))
            {
                query = string.Format("var items = jQuery(\"{0} option\");var text = \"\"; for(var i=0; i<items.length;i++)", path) +
                    "{var line = jQuery(items[i]).attr('value')+\"}\"+jQuery(items[i]).attr('title')+\"}\"+" +
                    "jQuery(items[i]).text().trim();text +=line+\";\"} return text;";
            }
            else
            {
                query = string.Format("var items = {0}.find(\"option\");var text = \"\"; for(var i=0; i<items.length;i++)", path) +
                    "{var line = jQuery(items[i]).attr('value')+\"}\"+jQuery(items[i]).attr('title')+\"}\"+" +
                    "jQuery(items[i]).text().trim();text +=line+\";\"} return text;";
            }
            return query;
        }

        public static string GetRefreshPageScript()
        {
            return "var loc=window.location;jQuery(function(){window.location=loc;});";
        }

        public static string GetRefreshPageScript(string url)
        {
            return string.Format("jQuery(function(){{window.location='{0}';}});", url);
        }

        public static string GetGetCurrentUrlScript()
        {
            return "return  $(location).attr('href')";
        }

        public static string GetSelectedIndexScript(string path)
        {            
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("return jQuery(\"{0}\").prop(\"selectedIndex\")", path);
            }
            else
            {
                return string.Format("return {0}.prop(\"selectedIndex\")", path);
            }
        }

        public static string GetCheckCheckBoxScript(string path)
        { 
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var check = jQuery(\"{0}\");check.prop(\"checked\", true); check.change();", path);
            }
            else
            {
               return string.Format("var check = {0};check.prop(\"checked\", true); check.change();", path);
            }
        }

        public static string GetCheckRadioButtonScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var check = jQuery(\"{0}\");check.prop(\"checked\", true); check.change();", path);
            }
            else
            {
                return string.Format("var check = {0};check.prop(\"checked\", true); check.change();", path);
            }
        }

        public static string GetUnCheckRadioButtonScript(string path)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var check = jQuery(\"{0}\");check.prop(\"checked\", false); check.change();", path);
            }
            else
            {
                return string.Format("var check = {0};check.prop(\"checked\", false); check.change();", path);
            }
        }

        public static string GetUnCheckCheckBoxScript(string path)
        {            
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var check = jQuery(\"{0}\");check.prop(\"checked\", false); check.change();", path);
            }
            else
            {
                return string.Format("var check = {0};check.prop(\"checked\", false); check.change();", path);
            }
        }

        public static string GetSelectByIndexScript(string path, int index)
        {           
            if (!path.StartsWith("jQuery"))
            {
                return string.Format("var ele = jQuery(\"{0}\"); ele.prop(\"selectedIndex\", {0}); ele.change()", path, index);
            }
            else
            {
                return string.Format("var ele = {0}; ele.prop(\"selectedIndex\", {0}); ele.change()", path, index);
            }
        }

        //Added on 01/16/2015
        public static string GetCSSColorScript(string path)
        {
            if (!path.StartsWith("jQuery"))
            {
                return string.Format(" return jQuery(\"{0}\").css('background-color')", path);
            }
            else
            {
                return string.Format(" return {0}.css('background-color')", path);
            }
        }

    }
}
