using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace JobExpressBack.Models.DTOs
{
    //authRepository.Login(model) est un objet générique de type object, et vous essayez d'accéder directement à ses propriétés (Message, Token, etc.), ce qui n'est pas possible sans un casting explicite ou une manipulation adéquate.
   //pour résoudre ce problème==> Définir une classe pour le retour de la méthode Login
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
