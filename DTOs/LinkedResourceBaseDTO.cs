using System.Collections.Generic;

namespace MottuLocation.DTOs
{
    /// <summary>
    /// Classe base para DTOs que suportam links HATEOAS.
    /// </summary>
    public abstract class LinkedResourceBaseDTO
    {
        /// <summary>
        /// Coleção de links de hipermídia relacionados ao recurso.
        /// </summary>
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}