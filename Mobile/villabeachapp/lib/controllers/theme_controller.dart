import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/service/auth_service.dart';

class ThemeController extends GetxController {
  RxString themeText = ''.obs;
  //late FirebaseFirestore db = DBFirestore.get();
  late AuthService authService = AuthService();

  Map<String, ThemeMode> themeModes = {
    'light': ThemeMode.light,
    'dark': ThemeMode.dark,
    'system': ThemeMode.system,
  };

  static ThemeController get to => Get.find();

  loadThemeMode() async {
    if (AuthService.to.userIsAuthenticated.value) {
      // final snapshot = await db
      //     .collection('user')
      //     .doc(AuthService.to.user!.uid)
      //     .collection('preferences')
      //     .doc('settings')
      //     .get();

      // themeText.value = snapshot.get('themeMode');
    } else {
      themeText.value = 'system';
    }
    setMode(themeText.value);
  }

  Future setMode(String theme) async {
    ThemeMode? themeMode = themeModes[theme];
    Get.changeThemeMode(themeMode ?? ThemeMode.system);
    if (AuthService.to.userIsAuthenticated.value) {
      // await db
      //     .collection('user')
      //     .doc(AuthService.to.user!.uid)
      //     .collection('preferences')
      //     .doc('settings')
      //     .set({'themeMode': theme});
    }
  }

  changeTheme(String theme) {
    setMode(theme);
    themeText.value = theme;
  }
}
