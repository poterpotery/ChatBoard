/* eslint-disable prettier/prettier */
/* eslint-disable react/jsx-no-undef */
import React from 'react';
import {
  TouchableOpacity,
  Image,
  StyleSheet,
  ActivityIndicator,
  View,
  Text
} from 'react-native';
import { useTheme } from '../../hooks';
import LinearGradient from 'react-native-linear-gradient';
import { APP_COLORS } from '../../utils/colorContants';

export function GoogleSignInBtn({ onPress, loader, Apple, disabled }) {
  const { Images } = useTheme()
  return (
    <TouchableOpacity
      activeOpacity={0.8}
      onPress={onPress}
      disabled={disabled}
    >
      <LinearGradient
        colors={[APP_COLORS.gradientDark, APP_COLORS.gradientLight]}
        style={[styles.btnContainer]}
        >


        {loader ? (
          <ActivityIndicator size="small" color={'white'} />
        ) : (
          <View
            style={{
              height: "100%",
              flexDirection: "row",
              alignItems: 'center',
              justifyContent: 'center',
            }}>
            <Image source={Apple ? Images.icon.gmail : Images.icon.gmail} style={styles.icon} />
            <Text style={[styles.text, { fontWeight: 600 }]}>Continue with Google</Text>
          </View>
        )}
      </LinearGradient>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  btnContainer: {
    height: 55,
    borderRadius: 17,
    width:"100%",
    paddingHorizontal:30
  },
  icon: { height: 30, width: 30, resizeMode: 'contain', marginRight: 10 },
  text: {
    color: 'white',
    fontSize: 18,
    fontWeight: "bold",
    marginLeft: 5

  },
});
