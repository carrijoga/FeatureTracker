using System.Text;
using FeatureTracker.Domain.Model.Users;

namespace FeatureTracker.Domain.View.Email.Users;

public class RecoveryPasswordEmail : EmailMessage
{
    #region Constants
    private string reset_link = "https://www.resetlink.com/";
    private string unsubscribe_link = "https://www.unsubscribelink.com";
    private string privacy_policy = "https://www.privacypolicy.com";
    #endregion

    #region Constructor

    public RecoveryPasswordEmail(User user)
    {
        To = user.Email;
        Subject = "FeatureTracker | Password Recovery!";
        FromName = "FeatureTracker Support";
    }

    #endregion

    #region Methods

    public string GetMessageEmail(User user)
    {
        var messageEmail = new StringBuilder();
        messageEmail.AppendLine($"<!DOCTYPE html>");
        messageEmail.AppendLine($"<html lang='en'>");
        messageEmail.AppendLine($"<head>");
        messageEmail.AppendLine($"    <meta charset='UTF-8'>");
        messageEmail.AppendLine($"    <meta name='viewport' content='width=device-width, initial-scale=1.0'>");
        messageEmail.AppendLine($"    <title>Reset Your Car Care Password</title>");
        messageEmail.AppendLine(GetStyleEmail());
        messageEmail.AppendLine($"</head>");
        messageEmail.AppendLine($"<body>");
        messageEmail.AppendLine($"    <div class='container'>");
        messageEmail.AppendLine($"        <div class='header'>");
        messageEmail.AppendLine($"            <h1>Car Care</h1>");
        messageEmail.AppendLine($"        </div>");
        messageEmail.AppendLine($"        <div class='content'>");
        messageEmail.AppendLine($"            <h2>Password Reset Request</h2>");
        messageEmail.AppendLine($"            <p>Hello {user.Person},</p>");
        messageEmail.AppendLine($"            <p>We received a request to reset your password for your Car Care account. If you did not make this request, you can ignore this email and your password will remain unchanged.</p>");
        messageEmail.AppendLine($"            ");
        messageEmail.AppendLine($"            <p>To reset your password, click on the button below:</p>");
        messageEmail.AppendLine($"            ");
        messageEmail.AppendLine($"            <center>");
        messageEmail.AppendLine($"                <a href='{reset_link}' class='button'>Reset Password</a>");
        messageEmail.AppendLine($"            </center>");
        messageEmail.AppendLine($"            ");
        messageEmail.AppendLine($"            <p>Or copy and paste the following link into your browser:</p>");
        messageEmail.AppendLine($"            <p style='word-break: break-all;'>{reset_link}</p>");
        messageEmail.AppendLine($"            ");
        messageEmail.AppendLine($"            <div class='safety-tip'>");
        messageEmail.AppendLine($"                <strong>Security Tip:</strong> For your protection, this password reset link will expire in 24 hours. Never share your password or this email with anyone.");
        messageEmail.AppendLine($"            </div>");
        messageEmail.AppendLine($"            ");
        messageEmail.AppendLine($"            <p>If you're having trouble, please contact our support team at <a href='mailto:support@featuretracker.com'>support@featuretracker.com</a></p>");
        messageEmail.AppendLine($"        </div>");
        messageEmail.AppendLine($"        <div class='footer'>");
        messageEmail.AppendLine($"            <p>&copy; 2025 FeatureTracker. All rights reserved.</p>");
        messageEmail.AppendLine($"            <!-- <p>Brazil</p> -->");
        messageEmail.AppendLine($"            <p><a href='{unsubscribe_link}'>Unsubscribe</a> | <a href='{privacy_policy}'>Privacy Policy</a></p>");
        messageEmail.AppendLine($"        </div>");
        messageEmail.AppendLine($"    </div>");
        messageEmail.AppendLine($"</body>");
        messageEmail.AppendLine($"</html>");

        return messageEmail.ToString();
    }

    private string GetStyleEmail()
    {
        StringBuilder styleMessage = new();

        styleMessage.AppendLine("    <style>");
        styleMessage.Append("        body {");
        styleMessage.Append("            font-family: Arial, sans-serif;");
        styleMessage.Append("            line-height: 1.6;");
        styleMessage.Append("            color: #333333;");
        styleMessage.Append("            margin: 0;");
        styleMessage.Append("            padding: 0;");
        styleMessage.Append("            background-color: #f4f4f4;");
        styleMessage.Append("        }");
        styleMessage.Append("        .container {");
        styleMessage.Append("            max-width: 600px;");
        styleMessage.Append("            margin: 0 auto;");
        styleMessage.Append("            padding: 20px;");
        styleMessage.Append("            background-color: #ffffff;");
        styleMessage.Append("        }");
        styleMessage.Append("        .header {");
        styleMessage.Append("            text-align: center;");
        styleMessage.Append("            padding: 20px 0;");
        styleMessage.Append("            background-color: #1a73e8;");
        styleMessage.Append("            color: white;");
        styleMessage.Append("        }");
        styleMessage.Append("        .content {");
        styleMessage.Append("            padding: 20px;");
        styleMessage.Append("        }");
        styleMessage.Append("        .button {");
        styleMessage.Append("            display: inline-block;");
        styleMessage.Append("            padding: 12px 24px;");
        styleMessage.Append("            background-color: #1a73e8;");
        styleMessage.Append("            color: white;");
        styleMessage.Append("            text-decoration: none;");
        styleMessage.Append("            border-radius: 4px;");
        styleMessage.Append("            font-weight: bold;");
        styleMessage.Append("            margin: 20px 0;");
        styleMessage.Append("        }");
        styleMessage.Append("        .button:hover {");
        styleMessage.Append("            background-color: #0d47a1;");
        styleMessage.Append("        }");
        styleMessage.Append("        .footer {");
        styleMessage.Append("            text-align: center;");
        styleMessage.Append("            padding: 20px;");
        styleMessage.Append("            font-size: 12px;");
        styleMessage.Append("            color: #666666;");
        styleMessage.Append("            border-top: 1px solid #eeeeee;");
        styleMessage.Append("        }");
        styleMessage.Append("        .safety-tip {");
        styleMessage.Append("            background-color: #f5f5f5;");
        styleMessage.Append("            padding: 15px;");
        styleMessage.Append("            border-left: 4px solid #ffd700;");
        styleMessage.Append("            margin: 20px 0;");
        styleMessage.Append("        }");
        styleMessage.Append("        @media screen and (max-width: 480px) {");
        styleMessage.Append("            .container {");
        styleMessage.Append("                width: 100%;");
        styleMessage.Append("            }");
        styleMessage.Append("            .button {");
        styleMessage.Append("                display: block;");
        styleMessage.Append("                text-align: center;");
        styleMessage.Append("            }");
        styleMessage.Append("        }");
        styleMessage.AppendLine("    </style>");

        return styleMessage.ToString();
    }

    #endregion
}
