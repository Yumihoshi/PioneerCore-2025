// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/05/03 18:37
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Yumihoshi.Entities
{
    public class TilemapLayer : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            _tilemap.color = Color.clear;
        }
    }
}
