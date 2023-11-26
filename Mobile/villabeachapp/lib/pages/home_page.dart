import 'package:flutter/material.dart';
import 'package:villabeachapp/service/auth_service.dart';
import 'package:villabeachapp/widgets/admin_appbar.dart';
import 'package:villabeachapp/widgets/custumer_appbar.dart';
import 'package:villabeachapp/widgets/navbar.dart';

class HomePage extends StatelessWidget {
  HomePage({super.key});

  final bool isAdmin = AuthService.to.user?.role == 'admin';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: isAdmin ? AdminAppBar() : CustumerAppBar(),
      drawer: isAdmin ? const NavBar() : null,
    );
  }
}
