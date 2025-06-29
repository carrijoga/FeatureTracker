using FeatureTracker.Domain.Enums.Users;

namespace FeatureTracker.Domain.Model.Users;

public class UserInvite
{
    #region Constructor
    public UserInvite() { }
    #endregion

    #region Proprieties
    public int UserInviteId { get; set; }
    public string Email { get; set; }
    public Guid Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsUsed { get; set; }
    public InviteStatus InviteStatus { get; set; }

    public string GetMessageEmail(string inviteToken, string creatorName)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"pt-BR\">");
        sb.AppendLine("<head>");
        sb.AppendLine("<meta charset=\"UTF-8\">");
        sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine("<title>Convite para Cadastro</title>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 40px;\">");
        sb.AppendLine("<div style=\"max-width: 520px; margin: auto; background: #ffffff; border-radius: 8px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); padding: 32px;\">");

        sb.AppendLine("<div style=\"text-align: center; margin-bottom: 24px;\">");
        sb.AppendLine("<img src=\"https://imgur.com/a/GsW9NcU\" alt=\"Logo\" style=\"max-width: 150px;\">");
        sb.AppendLine("</div>");

        sb.AppendLine("<h2 style=\"color: #333333; text-align: center;\">Você foi convidado para se cadastrar!</h2>");
        sb.AppendLine("<p>Olá,</p>");
        sb.AppendLine("<p>Você recebeu um convite para se cadastrar no nosso sistema. Para continuar com o cadastro, clique no botão abaixo:</p>");

        sb.AppendLine("<div style=\"text-align: center; margin: 32px 0;\">");
        sb.AppendLine($"<a href=\"https://localhost:8081/register?token={inviteToken.ToLower()}\" style=\"display: inline-block; background-color: #007bff; color: #ffffff; padding: 14px 32px; border-radius: 6px; text-decoration: none; font-size: 16px;\">Continuar Cadastro</a>");
        sb.AppendLine("</div>");

        sb.AppendLine("<p>Se você não solicitou este convite, pode simplesmente ignorar este e-mail.</p>");
        sb.AppendLine($"<p style=\"font-size: 12px; color: #888888;\">Convite enviado por: {creatorName}</p>");

        sb.AppendLine("</div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
    #endregion
}
