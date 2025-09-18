namespace MottuLocation.DTOs
{
    /// <summary>
    /// Representa um link de hipermídia (HATEOAS).
    /// </summary>
    public class LinkDTO
    {
        /// <summary>
        /// A URL do recurso.
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// A relação do link com o recurso atual (ex: "self", "update_moto").
        /// </summary>
        public string Rel { get; private set; }

        /// <summary>
        /// O método HTTP a ser usado para acessar o recurso (ex: "GET", "POST").
        /// </summary>
        public string Method { get; private set; }

        public LinkDTO(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}