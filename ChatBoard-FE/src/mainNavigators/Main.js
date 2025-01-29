import React, { useEffect, } from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { useNavigation } from '@react-navigation/native';
import AuthNavigator from './stacks/AuthenticationStacks/AuthNavigator';
import DrawerNavigation from './stacks/DrawerNavigations/DrawerNavigation';
import SplashScreen from '../screens/SplashScreen/SplashScreen';
import { StatusBar } from 'react-native';


const Stack = createNativeStackNavigator();
// @refresh reset
const MainNavigator = () => {
    const navigation = useNavigation()
    return (
        <>

            <Stack.Navigator
                screenOptions={{
                    headerShown: false,
                    gestureEnabled: true,
                    gestureDirection: 'horizontal',
                    cardStyleInterpolator: ({ current, layouts }) => {
                        return {
                            cardStyle: {
                                transform: [
                                    {
                                        translateX: current.progress.interpolate({
                                            inputRange: [0, 1],
                                            outputRange: [layouts.screen.width, 0],
                                        }),
                                    },
                                ],
                            },
                        };
                    },
                }}
            >
                {/* <Stack.Screen name="SplashScreen" component={SplashScreen} /> */}
                <Stack.Screen name="Authentication" component={AuthNavigator} />
                <Stack.Screen name="DrawerNavigation" component={DrawerNavigation} />


            </Stack.Navigator>

        </>
    );
};
export default MainNavigator;
