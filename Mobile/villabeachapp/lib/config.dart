import "package:flutter/material.dart";
import "package:get/get.dart";
import "package:villabeachapp/controllers/theme_controller.dart";
import "package:villabeachapp/controllers/users_controller.dart";
import "package:villabeachapp/service/auth_service.dart";
import "package:villabeachapp/service/user_service.dart";
import "package:villabeachapp/service/user_settings/user_settings_service.dart";

initConfigurations() async {
  WidgetsFlutterBinding.ensureInitialized();

  Get.lazyPut<AuthService>(() => AuthService());
  Get.lazyPut<ThemeController>(() => ThemeController());
  Get.lazyPut<UserSettingsService>(() => UserSettingsService());
  Get.lazyPut<UserService>(() => UserService());
  Get.lazyPut<UsersController>(() => UsersController());
}
