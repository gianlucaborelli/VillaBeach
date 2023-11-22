import 'package:get/get.dart';

class AuthService extends GetxController {
  var userIsAuthenticated = false.obs;

  @override
  void onInit() {
    super.onInit();
  }

  static AuthService get to => Get.find<AuthService>();

  createUser(String email, String password, String name) async {}

  login(String email, String password) async {}

  resetPassword(String email) async {}

  logout() async {}

  Future<void> sendEmailVerification() async {}
}
