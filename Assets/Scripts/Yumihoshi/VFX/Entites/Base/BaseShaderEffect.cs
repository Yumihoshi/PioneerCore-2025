// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/13 11:04
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;

namespace Yumihoshi.VFX.Entites.Base
{
    public class BaseShaderEffect
    {
        private const string SHADER_PATH_PREFIX = "Shader Graphs/";
        protected Material mat;

        protected BaseShaderEffect(string shaderName)
        {
            Shader shader = Shader.Find(GetShaderPath(shaderName));
            if (!shader)
            {
                Debug.LogError("[VFX] 未找到Shader：" + shaderName);
                return;
            }

            FullShaderName = GetShaderPath(shaderName);
            mat = new Material(shader);
        }

        public string FullShaderName { get; private set; }

        /// <summary>
        /// 获取Shader的路径
        /// </summary>
        /// <param name="shaderName"></param>
        /// <returns></returns>
        protected string GetShaderPath(string shaderName)
        {
            return SHADER_PATH_PREFIX + shaderName;
        }

        /// <summary>
        /// 将Shader应用到GameObject上
        /// </summary>
        /// <param name="obj">要应用材质效果的物体</param>
        public void ApplyToGameObject(GameObject obj)
        {
            if (!mat)
            {
                Debug.LogError("[VFX] 未找到Shader：" + mat.name);
                return;
            }

            obj.GetComponent<Renderer>().material = mat;
        }
    }
}
