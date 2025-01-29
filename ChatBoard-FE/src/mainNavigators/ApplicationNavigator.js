import React from 'react';
import {  SafeAreaView, StatusBar, StyleSheet } from 'react-native';
import {
  NavigationContainer,
  useNavigationContainerRef,
} from '@react-navigation/native';
import { useTheme } from '../hooks';
import MainNavigator from './Main.js';
import { Colors } from '../theme/Variables';
// import { BottomSheetModalProvider } from '@gorhom/bottom-sheet';
import { GestureHandlerRootView } from 'react-native-gesture-handler';


const ApplicationNavigator = () => {
  const { Layout,  } = useTheme();
  return (
    <>
        <SafeAreaView style={[Layout.fill]}>
          <GestureHandlerRootView style={{ flex: 1 }}>
            {/* <BottomSheetModalProvider> */}
              <NavigationContainer>
               
                <MainNavigator />
              </NavigationContainer>
            {/* </BottomSheetModalProvider> */}
          </GestureHandlerRootView>
        </SafeAreaView>
    </>
  );
};

export default ApplicationNavigator;

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
    fontWeight: 'bold',
    marginLeft: 20, // Adjust margins or styling as needed
    backgroundColor: 'pink',
    width: 200,
  },
  drawerStyle: {
    backgroundColor: Colors.white,
    width: '85%',
    borderTopLeftRadius: 30,
    borderBottomLeftRadius: 30,
    borderColor: '#f0f0f0',
    borderWidth: 3,
  },
});
