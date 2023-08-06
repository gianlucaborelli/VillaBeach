import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/pages/home_page.dart';
import 'package:villabeachapp/pages/login_page.dart';

import '../service/auth_service.dart';

class CheckAuth extends StatelessWidget {
  const CheckAuth({super.key});
  final bool userIsAuthenticated = false;

  @override
  Widget build(BuildContext context) {
    return Obx(() => AuthService.to.userIsAuthenticated.value
        ? const HomePage()
        : const LoginPage());
  }
}
