import 'dart:convert';

import 'package:get/get.dart';
import 'package:http/http.dart';
import 'package:villabeachapp/model/user_model.dart';
import 'package:villabeachapp/security/secure_storage.dart';
import 'package:villabeachapp/utility/webservice_url.dart';

class UserService extends GetxController {
  static UserService get to => Get.find<UserService>();

  Future<List<UserModel>> getAllUsers() async {
    var url = WebServiceUrl.baseUserUrl;
    var token = await TokenSecureStore().getAccessTokens();

    var response = await get(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token'
      },
    );

    var list = jsonDecode(response.body) as List;
    return list.map((json) => UserModel.fromJson(json)).toList();
  }
}
