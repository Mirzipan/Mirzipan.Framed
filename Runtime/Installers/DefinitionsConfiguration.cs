using UnityEngine;

namespace Mirzipan.Framed.Installers
{
    [CreateAssetMenu(fileName = "DefinitionsConfiguration", menuName = "Framed/Definitions Configuration", order = 10010)]
    public class DefinitionsConfiguration : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Path relative to a Resources folder.")]
        private string _pathToLoadFrom = "Data";

        public string PathToLoadFrom => _pathToLoadFrom;
    }
}