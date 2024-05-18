public static class EmailModels
{
    public static string EmailValidation = @"
    <!DOCTYPE html>
    <html lang=""en"">
        <head>
            <meta charset = ""UTF-8"" >
            < meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Confirmação de Email</title>
            <style>
                body {
                    font-family: 'Arial', sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }

                .container {
                    max-width: 600px;
                    margin: 20px auto;
                    background - color: #ffffff;
                    padding: 20px;
                    border - radius: 8px;
                    box - shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }

                h1, h2, p {
                    color: #333;
                }

                button {
                    display: inline - block;
                    padding: 10px 20px;
                    font - size: 16px;
                    background - color: #007BFF;
                    color: #fff;
                    text - decoration: none;
                    border: none;
                    border - radius: 4px;
                    cursor: pointer;
                }

                button: hover {
                    background - color: #0056b3;
                }
            </ style >
        </ head >
        < body >
            < div class= ""container"" >
                < h1 > Olá, {{USER_NAME}}!</ h1 >
                < h2 > Obrigado por realizar o cadastro em nosso APP!</h2>
                <p>Seu endereço de e-mail foi registrado. Para confirmar e ativar sua conta, clique no botão abaixo:</ p >
                < a href = ""https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/verify_email?token={{LINK_VALIDATION}}"" >
                    < button type = ""button"" > Confirmar Email </ button >
                </ a >
                < p > Ou se preferir, copie e cole o link a seguir em seu navegador:</ p >
                < p > https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/verify_email?token={{LINK_VALIDATION}}</p>
            </ div >
        </ body >
    </ html > ";    

    public static string ForgotPassword = @"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <title>Recuperação de Senha</title>
        <style>
            body {
                font-family: 'Arial', sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }

            .container {
                max-width: 600px;
                margin: 20px auto;
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }

            h1, h2, p {
                color: #333;
            }

            button {
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                background-color: #007BFF;
                color: #fff;
                text-decoration: none;
                border: none;
                border-radius: 4px;
                cursor: pointer;
            }

            button:hover {
                background-color: #0056b3;
            }
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Olá, {{USER_NAME}}!</h1>
            <h2>Esqueceu sua senha?</h2>
            <p>Recebemos uma solicitação para redefinir a senha associada à sua conta. Se você não fez essa solicitação, pode ignorar este e-mail.</p>
            <p>Para redefinir sua senha, clique no botão abaixo:</p>
            <a href=""https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/reset_password?token={{FORGOT_TOKEN}}"">
                <button type=""button"">Redefinir Senha</button>
            </a>
            <p>Ou copie e cole o link a seguir em seu navegador:</p>
            <p>https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/reset_password?token={{FORGOT_TOKEN}}</p>
            <p>Este link de redefinição de senha expirará em 1 dia.</p>
        </div>
    </body>
    </html>";
}