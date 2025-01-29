import React, { useEffect, useRef, useState } from 'react';
import { PermissionsAndroid, StyleSheet, Platform, View, Dimensions, BackHandler } from 'react-native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { useTheme } from '../../../hooks';
import { useNavigation } from '@react-navigation/native';
import { CustomDrawerContent } from '../../../components/CustomDrawer/CustomDrawer';
import { BottomNavigator } from '../BottomNavigator/BottomNavigator';


const Drawer = createDrawerNavigator();
// @refresh reset

const DrawerNavigation = () => {
  const { Layout, darkMode, NavigationTheme, Images, CustomColors } =
    useTheme();
  const navigation = useNavigation()




  return (
    <View style={{ flex: 1, overflow: 'hidden' }}>
      <Drawer.Navigator
      
        screenOptions={{
          drawerPosition: 'left',
          headerShown: false,
          gestureEnabled: true,
          gestureDirection: 'horizontal',
          drawerStyle: [
            styles.drawerStyle,
            {
              width: "1%",
              backgroundColor: darkMode ? CustomColors.DMBackgroundPrimaryColor : 'white',
              borderRightColor: darkMode ? CustomColors.DMBorderColor : "#E7E7E7",
            }],
        }}
        initialRouteName={null}
        drawerContent={props => {
          return (
            <CustomDrawerContent {...props} />
          )
        }}

      >
        <Drawer.Screen
          name="BottomNavigation"
          component={BottomNavigator}
        // initialParams={{ screenName: 'Home' }}
        />
      </Drawer.Navigator>
    </View>

  );
};

export default DrawerNavigation;

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
    // paddingTop: 40, // Adjust padding or styling as needed

    alignItems: 'center',
    alignContent: 'center',
    paddingVertical: 25,
    // paddingHorizontal:30
    // justifyContent:'center'
  },
  DrawerTabWidth: { width: '80%' },
  title: {
    fontSize: 20,

    marginLeft: 20,
    width: 200,
  },
  drawerStyle: {
    // borderTopRightRadius: 30,
    // borderBottomRightRadius: 30,
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
    ...Platform.select({
      ios: {
        borderTopRightRadius: 0,
        borderBottomRightRadius: 0,
      }
    })
  },
});
