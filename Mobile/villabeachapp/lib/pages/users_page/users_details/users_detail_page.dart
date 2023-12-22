import 'package:flutter/material.dart';
import 'package:get/get_state_manager/src/rx_flutter/rx_obx_widget.dart';
import 'package:villabeachapp/controllers/user_detail_controller.dart';
import 'package:villabeachapp/utility/responsive.dart';

class UserDetailPage extends StatefulWidget {
  final String? user;

  const UserDetailPage({super.key, this.user});

  @override
  State<UserDetailPage> createState() => _UserDetailPageState();
}

class _UserDetailPageState extends State<UserDetailPage> {
  @override
  void initState() {
    if (widget.user != null) {
      UserDetailController.to.loadUser(widget.user!);
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Obx(
      () {
        return UserDetailController.to.user == null
            ? Container()
            : Responsive(
                mobile: Scaffold(
                  appBar: AppBar(
                    title: const Text('Usuário'),
                    centerTitle: true,
                  ),
                  body: Text(UserDetailController.to.user!.name!),
                ),
                desktop: Scaffold(
                  body: Text(UserDetailController.to.user!.name!),
                ),
              );
      },
    );
  }
}
