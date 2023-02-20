using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Utils.Web.Helpers;

/// <summary>
/// MVC帮助类
/// </summary>
[SingletonInject]
public class MvcHelper
{
    private List<ControllerDescriptor> _controllerDescriptors;
    private List<ActionDescriptor> _actionDescriptors;
    private readonly ApplicationPartManager _partManager;
    private static readonly object LockObject = new object();

    public MvcHelper(ApplicationPartManager partManager)
    {
        _partManager = partManager;
    }

    /// <summary>
    /// 获取所有Controller，不包含抽象的类
    /// </summary>
    /// <returns></returns>
    public List<ControllerDescriptor> GetAllController()
    {
        lock (LockObject)
        {
            if (_controllerDescriptors != null && _controllerDescriptors.Any())
                return _controllerDescriptors;

            _controllerDescriptors = new List<ControllerDescriptor>();

            var controllerFeature = new ControllerFeature();
            _partManager.PopulateFeature(controllerFeature);
            var controllerTypes = controllerFeature.Controllers.ToList();
            foreach (var typeInfo in controllerTypes)
            {
                if (typeInfo.IsAbstract)
                    continue;

                var controller = new ControllerDescriptor
                {
                    Name = typeInfo.Name.Replace("Controller", ""),
                    TypeInfo = typeInfo
                };

                var areaAttr = (AreaAttribute)Attribute.GetCustomAttribute(typeInfo, typeof(AreaAttribute));
                if (areaAttr != null)
                {
                    controller.Area = areaAttr.RouteValue;
                }

                var descAttr =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(typeInfo, typeof(DescriptionAttribute));
                if (descAttr != null && descAttr.Description.NotNull())
                {
                    controller.Description = descAttr.Description;
                }

                _controllerDescriptors.Add(controller);
            }

            return _controllerDescriptors;
        }
    }

    /// <summary>
    /// 反射所有的action
    /// </summary>
    /// <returns></returns>
    public List<ActionDescriptor> GetAllAction()
    {
        lock (LockObject)
        {
            if (_actionDescriptors != null && _actionDescriptors.Any())
                return _actionDescriptors;

            _actionDescriptors = new List<ActionDescriptor>();

            var controllers = GetAllController();
            foreach (var controller in controllers)
            {
                foreach (var method in controller.TypeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (!method.CustomAttributes.Any(m =>
                        m.AttributeType == typeof(HttpGetAttribute)
                        || m.AttributeType == typeof(HttpPostAttribute)
                        || m.AttributeType == typeof(HttpPutAttribute)
                        || m.AttributeType == typeof(HttpOptionsAttribute)
                        || m.AttributeType == typeof(HttpHeadAttribute)
                        || m.AttributeType == typeof(HttpPatchAttribute)
                        || m.AttributeType == typeof(HttpDeleteAttribute)))
                        continue;

                    var action = new ActionDescriptor
                    {
                        Controller = controller,
                        MethodInfo = method,
                        Name = method.Name
                    };

                    var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(method, typeof(DescriptionAttribute));
                    if (descAttr != null && descAttr.Description.NotNull())
                    {
                        action.Description = descAttr.Description;
                    }

                    _actionDescriptors.Add(action);
                }
            }

            return _actionDescriptors;
        }
    }
}
