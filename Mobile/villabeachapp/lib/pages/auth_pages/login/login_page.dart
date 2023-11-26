import 'package:flutter/material.dart';
import 'package:villabeachapp/pages/auth_pages/login/components/login_form.dart';
import 'package:villabeachapp/pages/auth_pages/login/components/login_top_image.dart';
import 'package:villabeachapp/utility/background.dart';
import 'package:villabeachapp/utility/responsive.dart';

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Background(
        child: SingleChildScrollView(
            child: Column(children: [
      Responsive(
          mobile: MobileLoginScreen(),
          desktop: Row(children: [
            Expanded(
              child: LoginTopImage(),
            ),
            Expanded(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  SizedBox(
                    width: 350,
                    child: LoginForm(),
                  ),
                ],
              ),
            )
          ])),
    ])));
  }
}

class MobileLoginScreen extends StatelessWidget {
  const MobileLoginScreen({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return const Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        LoginTopImage(),
        Row(
          children: [
            Spacer(),
            Expanded(
              flex: 8,
              child: LoginForm(),
            ),
            Spacer(),
          ],
        ),
      ],
    );
  }
}
