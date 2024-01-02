public static class EmailModels
{
    public static string EmailValidation = @"<head>
    <script>
        function submit() {            
            window.location.href = ""https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/verify_email?token={{LINK_VALIDATION}}"";
        }
    </script>
    </head>
    <body>
        <h1>
            Olá {{USER_NAME}}
        </h1>
        <h2>Obrigado por realizar o cadastro em nosso APP! </h2>
        <p>Seu endereço de e-mail foi registrado. Para confirmar e ativas sua conta, clique no botão abaixo:</p>
        <button type=""submit"">Confirmar Email.</button>
        <p>Ou se preferir copie e cole o link a seguir em seu navegador:</p>
        <p>https://villabeachwebservice1-1wjapvsj.b4a.run/api/users/verify_email?token={{LINK_VALIDATION}}</p>
    </body>";    
}