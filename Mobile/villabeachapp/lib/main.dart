import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/config.dart';
import 'package:villabeachapp/widgets/check_auth.dart';

void main() async {
  await initConfigurations();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData.from(
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color.fromARGB(255, 38, 124, 64),
        ),
      ),
      title: 'VillaBeach',
      home: const CheckAuth(),
    );
  }
}
