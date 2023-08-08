import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/service/auth_service.dart';

import '../controllers/autenticantion_controller.dart';

class NavBar extends StatelessWidget {
  NavBar({super.key});
  final controller = Get.put(AutenticacaoController());

  @override
  Widget build(BuildContext context) {
    String? photoURL = AuthService.to.user?.photoURL;

    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          UserAccountsDrawerHeader(
            accountName: Text(
              AuthService.to.user!.displayName == null
                  ? 'Cliente'
                  : AuthService.to.user!.displayName!,
              //style: Theme.of(context).textTheme.bodyMedium,
            ),
            accountEmail: Text(
              AuthService.to.user!.email!,
              //style: Theme.of(context).textTheme.bodyMedium,
            ),
            currentAccountPicture: CircleAvatar(
              child: ClipOval(
                child: Container(
                  child: photoURL != null
                      ? Image.network(
                          photoURL,
                          fit: BoxFit.cover,
                        )
                      : const FittedBox(
                          fit: BoxFit.fill,
                          child: Icon(
                            Icons.account_circle,
                            size: 100,
                          ),
                        ),
                ),
              ),
            ),
            decoration: const BoxDecoration(
              image: DecorationImage(
                  image: AssetImage('assets/images/appbar_image3.jpg'),
                  fit: BoxFit.cover),
            ),
          ),
          const Divider(),
          ListTile(
            leading: const Icon(Icons.settings),
            title: const Text('Configurações'),
            onTap: () => controller.logout(),
          ),
          ListTile(
            leading: const Icon(Icons.exit_to_app_outlined),
            title: const Text('Sair'),
            onTap: () => controller.logout(),
          )
        ],
      ),
    );
  }
}
