import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/config.dart';
import 'package:villabeachapp/controllers/theme_controller.dart';
import 'package:villabeachapp/theme/color_schemes.g.dart';
import 'package:villabeachapp/security/check_auth.dart';

void main() async {
  await initConfigurations();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    ThemeController.to.loadThemeMode();
    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(useMaterial3: true, colorScheme: lightColorScheme),
      darkTheme: ThemeData(useMaterial3: true, colorScheme: darkColorScheme),
      title: 'VillaBeach',
      home: const CheckAuth(),
    );
  }
}
