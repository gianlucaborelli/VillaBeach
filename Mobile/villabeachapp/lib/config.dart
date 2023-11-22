import "package:flutter/material.dart";
import "package:get/get.dart";
import "package:villabeachapp/controllers/theme_controller.dart";
import "package:villabeachapp/service/auth_service.dart";

initConfigurations() async {
  WidgetsFlutterBinding.ensureInitialized();

  Get.lazyPut<AuthService>(() => AuthService());
  Get.lazyPut<ThemeController>(() => ThemeController());
}
