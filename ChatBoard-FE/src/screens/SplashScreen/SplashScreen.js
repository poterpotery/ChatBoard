// SplashScreen.js
import React, { useEffect } from 'react';
import { View, Image, StyleSheet, Animated, StatusBar, Platform, PermissionsAndroid, Alert } from 'react-native';
import { LinearGradient } from 'react-native-linear-gradient';
import { Colors } from '../../theme/Variables';
import { APP_COLORS } from '../../utils/colorContants';
import { useNavigation } from '@react-navigation/native';
import { setUserLocation } from '../../store/userLocation';
import Geolocation from '@react-native-community/geolocation';
import { useDispatch } from 'react-redux';

const SplashScreen = () => {
    const imageScale = new Animated.Value(0.1);
    const navigation = useNavigation();


    Animated.timing(imageScale, {
        toValue: 1,
        duration: 1000,
        useNativeDriver: true,
    }).start();



    return (
        <LinearGradient
            colors={[APP_COLORS.gradientLight, APP_COLORS.gradientDark]}
            style={styles.container}

        >
            <StatusBar
                barStyle="light-content"
                backgroundColor="transparent"
                translucent={true}
            />

            <Animated.Image
                source={{
                    uri: 'https://99designs-blog.imgix.net/blog/wp-content/uploads/2022/06/Starbucks_Corporation_Logo_2011.svg-e1657703028844.png',
                }}
                style={[styles.image, { transform: [{ scale: imageScale }] }]}
            />
        </LinearGradient>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'white',
    },
    image: {
        width: 200,
        height: 200,
    },
});

export default SplashScreen;