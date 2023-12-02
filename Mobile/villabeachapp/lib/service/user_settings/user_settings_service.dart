import 'package:get/get.dart';
import 'package:http/http.dart';
import 'package:villabeachapp/security/secure_storage.dart';
import 'package:villabeachapp/utility/webservice_url.dart';

class UserSettingsService extends GetxController {
  static UserSettingsService get to => Get.find<UserSettingsService>();

  updateSettings(String settingsProperty, int settingsValue) async {
    var url = WebServiceUrl.settings;
    var token = await TokenSecureStore().getAccessTokens();

    var response = await post(
      Uri.parse('$url/$settingsProperty/value=$settingsValue'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token'
      },
    );

    if (response.statusCode != 200) {}
  }
}
