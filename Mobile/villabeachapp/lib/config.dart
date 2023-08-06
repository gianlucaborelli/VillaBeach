import "package:firebase_core/firebase_core.dart";
import "package:flutter/material.dart";
import "package:get/get.dart";
import "package:villabeachapp/service/auth_service.dart";

initConfigurations() async {
  WidgetsFlutterBinding.ensureInitialized();

  await Firebase.initializeApp();

  Get.lazyPut<AuthService>(() => AuthService());
}
